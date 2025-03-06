import { useEffect, useState } from "react"
import { layouts } from "@/scripts/dashboard_scripts/layouts"

export default function LayoutMenu({ setChosenLayout, setIsLayoutMenu }) {

    const [layoutList, setLayoutList] = useState([])

    useEffect(() => {
        let updatedLayoutList = [...layoutList]
        for (let layoutName in layouts) {
            updatedLayoutList.push(layoutName)
        }
        setLayoutList(updatedLayoutList)
    }, [layouts])

    return (
        <div onMouseOver={() => setIsLayoutMenu(true)} onMouseLeave={() => setIsLayoutMenu(false)} className="layout-menu">
            {layoutList.map(layoutName => (
                <button key={layoutName} onClick={() => setChosenLayout(layoutName)}>{layoutName}</button>
            ))}
        </div>
    )
}