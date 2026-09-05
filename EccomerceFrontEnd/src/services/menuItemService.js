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

  async getMenuItemById(id) {
    try {
      const response = await api.get(`/menuItem/${id}`)
      return response.data?.result || null
    } catch (error) {
      console.error('Error fetching menu item:', error)
      throw error
    }
  },

  async createMenuItem(formData) {
    try {
      const response = await api.post('/menuItem', formData)
      return response.data?.result || response.data
    } catch (error) {
      console.error('Error creating menu item:', error)
      throw error
    }
  },

  async updateMenuItem(id, formData) {
    try {
      const response = await api.put(`/menuItem/${id}`, formData)
      return response.data?.result || response.data
    } catch (error) {
      console.error('Error updating menu item:', error)
      throw error
    }
  },

  async deleteMenuItem(id) {
    try {
      const response = await api.delete(`/menuItem/${id}`)

      if (!response.data?.isSuccess) {
        throw new Error('Failed to delete menu item')
      }

      return response.data
    } catch (error) {
      console.error('Error deleting menu item:', error)
      throw error
    }
  },
}
