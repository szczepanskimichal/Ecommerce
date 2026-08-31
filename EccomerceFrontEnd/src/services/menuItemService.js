import api from '@/services/api.js'

export default {
  async getMenuItems() {
    try {
      const response = await api.get('/menuItem')
      //console.log(response.data?.result)
      if (response.data?.result) {
        return response.data.result
      } else {
        console.error('Failed to fetch menu items', response.data)
      }
      return response.data?.result || []
    } catch (error) {
      console.error('Error fetching menu items:', error)
      throw error
    }
  },
}
