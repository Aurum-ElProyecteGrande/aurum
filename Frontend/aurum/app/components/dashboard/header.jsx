import { useState } from "react"

export default function Header({ setIsHamburgerOpen, isHamburgerOpen }) {

  const [username, setUsername] = useState("username")

  return (
    <div className="header">
      <div className="welcome">Hi {username}.</div>
      <button className="menu-button" onClick={() => setIsHamburgerOpen(!isHamburgerOpen)}> ... </button>
    </div>
  )
}