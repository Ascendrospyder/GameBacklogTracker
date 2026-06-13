import { Card, Flex, Layout, Typography } from 'antd'
import { ThemeToggle } from '../components/ThemeToggle'

const { Content } = Layout
const { Title, Paragraph } = Typography

type HomePageProps = {
  username: string
}

export function HomePage({ username }: HomePageProps) {
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
          style={{ width: '100%', maxWidth: 520, textAlign: 'center' }}
          styles={{ body: { padding: '48px 40px' } }}
        >
          <Flex vertical align="center" gap={12}>
            <Title level={2} style={{ margin: 0 }}>
              Welcome back, {username}
            </Title>
            <Paragraph type="secondary" style={{ margin: 0, fontSize: 16 }}>
              Your backlog dashboard is coming soon.
            </Paragraph>
          </Flex>
        </Card>
      </Content>
    </Layout>
  )
}
