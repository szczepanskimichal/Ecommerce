import { createApp } from 'vue'
import { createPinia } from 'pinia'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
import 'bootstrap-icons/font/bootstrap-icons.css'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import { useThemeStore } from '@/stores/themeStore.js'

import App from './App.vue'
import router from './router/routes.js'

const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)

const app = createApp(App)

app.use(pinia)
const themeStore = useThemeStore()
if (themeStore.theme) {
  document.body.setAttribute('data-bs-theme', themeStore.theme)
}

app.use(router)

app.mount('#app')
