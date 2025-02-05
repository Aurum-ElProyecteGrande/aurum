import { useState } from "react"
import LayoutMenu from "./layout-menu"

export default function HamburgerMenu({ isEditMode, setIsEditMode, choosenLayout, setChoosenLayout }) {

    const [isLayoutMenu, setIsLayoutMenu] = useState(false)

    return (
        <div className="hamburger-menu">
            <button onClick={() => setIsEditMode(!isEditMode)}>{isEditMode ? "Normal mode" : "Edit mode"}</button>
            <button onMouseOver={() => setIsLayoutMenu(true)} onMouseLeave={() => setIsLayoutMenu(false)} className={isLayoutMenu ? "active" : ""}>Change layout</button>
            {isLayoutMenu &&
                <LayoutMenu choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} setIsLayoutMenu={setIsLayoutMenu} />
            }
        </div>
    )
}