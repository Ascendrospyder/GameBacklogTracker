import { Button } from 'antd'
import { MoonOutlined, SunOutlined } from '@ant-design/icons'
import { useTheme } from '../context/ThemeContext'

export function ThemeToggle() {
  const { theme, toggleTheme } = useTheme()

  return (
    <Button
      type="text"
      shape="circle"
      size="large"
      icon={theme === 'dark' ? <SunOutlined /> : <MoonOutlined />}
      onClick={toggleTheme}
      aria-label={`Switch to ${theme === 'dark' ? 'light' : 'dark'} mode`}
      style={{
        position: 'fixed',
        top: 20,
        right: 20,
        zIndex: 10,
      }}
    />
  )
}
