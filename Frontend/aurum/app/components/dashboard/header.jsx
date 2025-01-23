import { useState } from "react"

export default function Header({ setIsHamburgerOpen, isHamburgerOpen, isEditMode }) {

  const [username, setUsername] = useState("username")

  return (
    <div className="header">
      <section>
        <p>Hi {username}.</p>
      </section>
      <section className="sub-title-section">
        <p>{isEditMode ? "Edit charts" : ""}</p>
      </section>
      <section className="menu-button-section">
        <button className="menu-button" onClick={() => setIsHamburgerOpen(!isHamburgerOpen)}> ... </button>
      </section>
    </div>
  )
}