import { apiUrl } from '../config'

export type User = {
  id: string
  steamId: string
  username: string
}

export async function getCurrentUser(): Promise<User | null> {
  const response = await fetch(`${apiUrl}/api/auth/me`, {
    credentials: 'include',
  })

  if (response.status === 401) {
    return null
  }

  if (!response.ok) {
    throw new Error('Failed to fetch current user')
  }

  return response.json()
}
