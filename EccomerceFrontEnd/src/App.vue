<script setup>
import { RouterView } from 'vue-router'
import Header from './components/layut/Header.vue'
import Footer from './components/layut/Footer.vue'
</script>

<template>
  <div class="app-shell">
    <Header />
    <main class="app-content">
      <RouterView />
    </main>
    <Footer />
  </div>
</template>

<style scoped>
.app-shell {
  background-color: var(--bs-body-bg);
  color: var(--bs-body-color);
  font-family: var(--bs-body-font-family);
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  width: 100%;
  transition:
    background-color 0.2s ease,
    color 0.2s ease;
}

.app-content {
  flex: 1;
  width: 100%;
}
</style>

<style>
body {
  background-color: var(--bs-body-bg);
  color: var(--bs-body-color);
  font-family: var(--bs-body-font-family);
  transition:
    background-color 0.2s ease,
    color 0.2s ease;
}
</style>
