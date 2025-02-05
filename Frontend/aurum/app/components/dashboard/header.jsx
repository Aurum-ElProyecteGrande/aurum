import { useEffect, useState } from "react"
import { CiMenuKebab } from "react-icons/ci";

export default function Header({ setIsHamburgerOpen, isHamburgerOpen, isEditMode }) {

  const [username, setUsername] = useState("username")

  useEffect(() => {
    const handleClick = (e) => {
      if (e.target.id !== "hamburger-menu-button") {
        setIsHamburgerOpen(false)
      }
    }

    window.addEventListener("click", (e) => handleClick(e))

    return (
      window.removeEventListener("click", (e) => handleClick(e))
    )
  }, [])

  return (
    <div className="header">
      <section>
        <p>hi {username}.</p>
      </section>
      <section className="sub-title-section">
        <p>{isEditMode ? "Edit charts" : ""}</p>
      </section>
      <section className="menu-button-section">
          <CiMenuKebab id="hamburger-menu-button" className="menu-button" onClick={() => setIsHamburgerOpen(!isHamburgerOpen)}/>
      </section>
    </div>
  )
}