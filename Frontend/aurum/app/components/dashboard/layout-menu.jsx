import { useEffect, useState } from "react"
import { layouts } from "../../../scripts/dashboard_scripts/dashboard_scripts"

export default function LayoutMenu({ setChoosenLayout, setIsLayoutMenu }) {

    const [layoutList, setLayoutList] = useState([])

    useEffect(() => {
        let updatedLayoutList = [...layoutList]
        for (let layoutName in layouts) {
            updatedLayoutList.push(layoutName)
        }
        console.log("layouts", layouts)
        console.log("layoutslist", layoutList)
        setLayoutList(updatedLayoutList)
    }, [layouts])

    return (
        <div onMouseOver={() => setIsLayoutMenu(true)} onMouseLeave={() => setIsLayoutMenu(false)} className="layout-menu">
            {layoutList.map(layoutName => (
                <button key={layoutName} onClick={() => setChoosenLayout(layoutName)}>{layoutName}</button>
            ))}
        </div>
    )
}