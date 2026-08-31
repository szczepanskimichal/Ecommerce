<script setup>
import { onMounted, ref } from 'vue'
import menuItemService from '@/services/menuItemService.js'

const menuItems = ref([])
const loading = ref(false)
const errorMessage = ref('')

const fetchMenuItems = async () => {
  loading.value = true
  errorMessage.value = ''
  try {
    menuItems.value = await menuItemService.getMenuItems()
    console.log(menuItems.value)
  } catch (error) {
    console.error('Error fetching menu items:', error)
    errorMessage.value =
      'Failed to load menu items. Please try again later. Check the console for more details.'
  } finally {
    loading.value = false
  }
}
onMounted(() => {
  fetchMenuItems()
})
</script>
<template>
  <h1>Menu Item List</h1>
  <div v-if="loading">Loading...</div>
  <div v-else-if="errorMessage" class="alert alert-warning" role="alert">{{ errorMessage }}</div>
  <ul v-else>
    <li v-for="item in menuItems" :key="item.id">{{ item.name }}</li>
  </ul>
</template>
