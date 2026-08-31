<template>
  <footer class="app-footer px-3 pb-3 pt-4">
    <div class="footer text-center mx-auto p-3 rounded-2">
      <p class="m-0">
        © 2024 Copyright:
        <a href="https://example.com/">example.com</a>
        Made with <i class="bi bi-heart-fill text-danger"></i> by
        <a href="https://example.com/">example.com</a>
      </p>
    </div>
  </footer>
</template>

<style scoped>
.app-footer {
  width: 100%;
}

.footer {
  max-width: 1200px;
  background-color: var(--bs-tertiary-bg);
  color: var(--bs-body-color);
  border: 1px solid var(--bs-border-color);
  transition:
    background-color 0.2s ease,
    color 0.2s ease,
    border-color 0.2s ease;
}

.footer a {
  color: var(--bs-link-color);
}
</style>
