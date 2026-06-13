import { Button } from 'antd'
import { apiUrl } from '../config'

function SteamIcon() {
  return (
    <svg
      viewBox="0 0 24 24"
      aria-hidden="true"
      width={20}
      height={20}
      style={{ display: 'block' }}
    >
      <path
        fill="currentColor"
        d="M11.979 0C5.678 0 .511 4.86.022 11.037l6.432 2.658c.545-.371 1.203-.59 1.912-.59.063 0 .125.004.188.006l2.861-4.142V8.841c0-2.485 2.017-4.5 4.503-4.5 2.484 0 4.5 2.015 4.5 4.5 0 2.484-2.016 4.5-4.5 4.5h-.105l-4.076 2.911c0 .052.004.105.004.159 0 1.875-1.515 3.396-3.39 3.396-1.635 0-3.016-1.173-3.331-2.727L.436 15.27C2.178 20.109 6.729 24 11.979 24c6.627 0 11.999-5.373 11.999-12S18.606 0 11.979 0zM7.54 18.21l-1.473-.61c.262.543.714.999 1.292 1.273 1.18.59 2.614.118 3.212-1.002.293-.588.381-1.251.217-1.897-.072-.285-.199-.553-.377-.787l1.36-1.942h.106c1.911 0 3.465-1.554 3.465-3.466 0-1.913-1.554-3.467-3.465-3.467-1.91 0-3.464 1.554-3.464 3.467v.105l-1.36 1.942a3.41 3.41 0 0 0-1.104-.181c-1.875 0-3.39 1.521-3.39 3.396 0 1.202.64 2.258 1.597 2.844.48.302 1.03.49 1.612.49.436 0 .86-.106 1.24-.304z"
      />
    </svg>
  )
}

export function SteamSignInButton() {
  const handleSignIn = () => {
    window.location.href = `${apiUrl}/api/auth/login`
  }

  return (
    <Button
      type="primary"
      size="large"
      icon={<SteamIcon />}
      onClick={handleSignIn}
      style={{
        background: '#171a21',
        height: 48,
        paddingInline: 28,
        fontWeight: 600,
      }}
      onMouseEnter={(e) => {
        e.currentTarget.style.background = '#1b2838'
      }}
      onMouseLeave={(e) => {
        e.currentTarget.style.background = '#171a21'
      }}
    >
      Sign in with Steam
    </Button>
  )
}
