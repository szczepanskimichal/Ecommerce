const defaultApiOrigin = 'https://localhost:7029'

export const CONFIG_API_URL = import.meta.env.DEV
  ? '/api'
  : import.meta.env.VITE_API_URL || `${defaultApiOrigin}/api`

export const CONFIG_API_IMAGE_URL = import.meta.env.VITE_API_IMAGE_URL || `${defaultApiOrigin}/`
