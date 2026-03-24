<script setup>
import { ref, onMounted } from "vue";
import { seedPost, getPost, createComment } from "../api/postApi";

const postId = "11111111-1111-1111-1111-111111111111";

const post = ref(null);
const commentText = ref("");
const loading = ref(false);
const submitting = ref(false);
const error = ref("");

const formatDisplayDate = (value) => {
  const date = new Date(value);

  const day = date.getDate();
  const month = date.toLocaleString("en-GB", { month: "long" });
  const year = date.getFullYear();

  const hours = String(date.getHours()).padStart(2, "0");
  const minutes = String(date.getMinutes()).padStart(2, "0");

  return `${day} ${month} ${year} ${hours}:${minutes}`;
};

const loadData = async () => {
  try {
    loading.value = true;
    error.value = "";

    await seedPost();
    const res = await getPost(postId);
    post.value = res.data;
  } catch (err) {
    console.error(err);
    error.value = "Failed to load post data.";
  } finally {
    loading.value = false;
  }
};

const submitComment = async () => {
  const msg = commentText.value.trim();
  if (!msg || !post.value || submitting.value) return;

  try {
    submitting.value = true;
    error.value = "";

    const res = await createComment(postId, {
      authorName: "Blend 285",
      message: msg
    });

    post.value.comments.push(res.data);
    commentText.value = "";
  } catch (err) {
    console.error(err);
    error.value = "Failed to create comment.";
  } finally {
    submitting.value = false;
  }
};

const onEnter = async (e) => {
  if (e.key === "Enter") {
    e.preventDefault();
    await submitComment();
  }
};

onMounted(loadData);
</script>

<template>
  <div class="page">
    <div v-if="loading" class="status-text">Loading...</div>

    <template v-else-if="post">
      <div class="header">{{ post.title }}</div>

      <div class="content">
        <div class="post-shell">
          <div class="author-row">
            <div class="avatar">C</div>
            <div class="author-meta">
              <div class="author-name">{{ post.authorName }}</div>
              <div class="author-date">{{ formatDisplayDate(post.createdAt) }}</div>
            </div>
          </div>

          <img :src="post.imageUrl" class="post-image" alt="post image" />

          <div class="input-row">
            <div class="avatar small">B</div>
            <div class="input-content">
              <div class="input-comment-name">Blend 285</div>
              <input
                v-model="commentText"
                @keydown="onEnter"
                placeholder="Comment"
                :disabled="submitting"
              />
            </div>
          </div>

          <div v-for="c in post.comments" :key="c.id" class="comment-row">
            <div class="avatar small">B</div>
            <div class="comment-content">
              <div class="comment-name">{{ c.authorName }}</div>
              <div class="comment-message">{{ c.message }}</div>
            </div>
          </div>
        </div>
      </div>
    </template>

    <div v-if="error" class="error-text">{{ error }}</div>
  </div>
</template>
<style scoped src="../assets/styles/post.css"></style>