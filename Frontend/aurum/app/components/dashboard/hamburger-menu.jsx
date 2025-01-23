import { useState } from "react"
import LayoutMenu from "./layout-menu"

export default function HamburgerMenu({ isEditMode, setIsEditMode, choosenLayout, setChoosenLayout }) {

    const [isLayoutMenu, setIsLayoutMenu] = useState(false)

    return (
        <div className="hamburger-menu">
            <button onClick={() => setIsEditMode(!isEditMode)}>{isEditMode ? "Normal mode" : "Edit mode"}</button>
            <button onClick={() => setIsLayoutMenu(!isLayoutMenu)}>Change layout</button>
            {isLayoutMenu &&
                <LayoutMenu choosenLayout={choosenLayout} setChoosenLayout={setChoosenLayout} />
            }
        </div>
    )
}