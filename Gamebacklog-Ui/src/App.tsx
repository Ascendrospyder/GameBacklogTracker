import { useEffect, useState } from 'react'
import { Flex, Spin } from 'antd'
import { getCurrentUser, type User } from './api/auth'
import { ThemeProvider } from './context/ThemeContext'
import { HomePage } from './pages/HomePage'
import { LoginPage } from './pages/LoginPage'

function AppContent() {
  const [user, setUser] = useState<User | null | undefined>(undefined)

  useEffect(() => {
    getCurrentUser()
      .then(setUser)
      .catch(() => setUser(null))
  }, [])

  if (user === undefined) {
    return (
      <Flex align="center" justify="center" style={{ minHeight: '100vh' }}>
        <Spin size="large" tip="Loading..." />
      </Flex>
    )
  }

  if (user) {
    return <HomePage username={user.username} />
  }

  return <LoginPage />
}

function App() {
  return (
    <ThemeProvider>
      <AppContent />
    </ThemeProvider>
  )
}

export default App
