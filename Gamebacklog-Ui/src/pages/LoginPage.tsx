import { Avatar, Card, Flex, Layout, Typography } from 'antd'
import { ThemeToggle } from '../components/ThemeToggle'
import { SteamSignInButton } from '../components/SteamSignInButton'

const { Content } = Layout
const { Title, Paragraph } = Typography

export function LoginPage() {
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <ThemeToggle />

      <Content
        style={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '2rem 1.5rem',
        }}
      >
        <Card
          style={{ width: '100%', maxWidth: 440, textAlign: 'center' }}
          styles={{ body: { padding: '48px 40px' } }}
        >
          <Flex vertical align="center" gap={24}>
            <Avatar
              size={72}
              style={{
                background: 'rgba(124, 58, 237, 0.15)',
                color: '#7c3aed',
                fontWeight: 700,
                fontSize: 24,
                border: '1px solid rgba(124, 58, 237, 0.35)',
              }}
            >
              GB
            </Avatar>

            <div>
              <Title level={2} style={{ marginBottom: 8 }}>
                Welcome to the GameBacklogTracker
              </Title>
              <Paragraph type="secondary" style={{ margin: 0, fontSize: 16 }}>
                Track your Steam backlog in one place
              </Paragraph>
            </div>

            <SteamSignInButton />
          </Flex>
        </Card>
      </Content>
    </Layout>
  )
}
