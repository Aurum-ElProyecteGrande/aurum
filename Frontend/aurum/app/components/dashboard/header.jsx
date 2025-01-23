export default function Header({ setIsHamburgerOpen, isHamburgerOpen }) {

    return (
      <div className="header">
        <button className="menu-button" onClick={() => setIsHamburgerOpen(!isHamburgerOpen)}> ... </button>
      </div>
    )
}