import { useEffect, useState } from "react"
import { CiMenuKebab } from "react-icons/ci";
import { FaRegSave } from "react-icons/fa";


export default function Header({ setIsHamburgerOpen, isHamburgerOpen, isEditMode, chosenLayout, saveChosenChartsForUser, username }) {

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
      <section >
        <p className="welcome">hi {username}.</p>
      </section>
      {isEditMode ?
        <section className="sub-title-section">
          <p>Edit charts</p>
          <div className="save-container" onClick={() => saveChosenChartsForUser()}>
            <FaRegSave className="save" />
            <div className="label">Save layout</div>
          </div>
        </section>
        :
        <section className="chosen-layout">
          <div>
            {chosenLayout} layout
          </div>
        </section>
      }
      <section className="menu-button-section">
        <CiMenuKebab id="hamburger-menu-button" className="menu-button" onClick={() => setIsHamburgerOpen(!isHamburgerOpen)} />
      </section>
    </div>
  )
}