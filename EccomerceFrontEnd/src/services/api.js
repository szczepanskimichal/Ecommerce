import axios from 'axios'
import { CONFIG_API_URL } from '@/constants/config.js'

const api = axios.create({
  baseURL: CONFIG_API_URL,
})

export default api
