import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useThemeStore = defineStore(
  'themeStore',
  () => {
    const theme = ref(document.documentElement.getAttribute('data-bs-theme') || 'light')

    function setTheme(newTheme) {
      theme.value = newTheme
      document.documentElement.setAttribute('data-bs-theme', newTheme)
      document.body.setAttribute('data-bs-theme', newTheme)
    }

    setTheme(theme.value)

    return { theme, setTheme }
  },
  {
    persist: true,
  },
)
