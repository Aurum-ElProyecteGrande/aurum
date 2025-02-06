import { useState } from "react"
import LayoutMenu from "./layout-menu"

export default function HamburgerMenu({ isEditMode, setIsEditMode, chosenLayout, setChosenLayout }) {

    const [isLayoutMenu, setIsLayoutMenu] = useState(false)

    return (
        <div className="hamburger-menu">
            <button onClick={() => setIsEditMode(!isEditMode)}>{isEditMode ? "Normal mode" : "Edit mode"}</button>
            <button onMouseOver={() => setIsLayoutMenu(true)} onMouseLeave={() => setIsLayoutMenu(false)} className={isLayoutMenu ? "active" : ""}>Change layout</button>
            {isLayoutMenu &&
                <LayoutMenu chosenLayout={chosenLayout} setChosenLayout={setChosenLayout} setIsLayoutMenu={setIsLayoutMenu} />
            }
        </div>
    )
}