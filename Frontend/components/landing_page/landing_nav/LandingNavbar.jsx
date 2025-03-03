"use client";
import React, { useState, useEffect } from "react";
import Link from "next/link";
import LandingLogo from "../landing_logo/LandingLogo";
import { IoMenuOutline, IoClose } from "react-icons/io5";


const LandingNavbar = ({ useModal, userInfo, handleLinkClick }) => {
	const [showNav, setShowNav] = useState(false);
	return (
		<header className="landing-header">
			<nav className="landing-navbar wrapper">
				<LandingLogo />
				<ul className={`${showNav ? "show" : ""}`}>
					<li onClick={() => setShowNav(false)}>
						<Link href="#prices">Prices</Link>
					</li>
					<li onClick={() => setShowNav(false)}>
						<Link href="#views">Views</Link>
					</li>
					<li onClick={() => setShowNav(false)}>
						<Link href="#footer">Socials</Link>
					</li>
					<li onClick={() => {
						setShowNav(false)
						handleLinkClick('/dashboard')
					}}>
						<Link href="/dashboard">Dashboard</Link>
					</li>
					<li onClick={() => {
						setShowNav(false)
						handleLinkClick('/transactions')
					}}>
						<Link href="/transactions">Transactions</Link>
					</li>
				</ul>
				<div className="landing-navbar-buttons">
					<button
						className="transparent-button"
						onClick={() => useModal(false)}>Login</button>
					<button className="accent-button"
						onClick={() => useModal(true)}>Register</button>
				</div>
				<div className="landing-navbar-menu" onClick={() => setShowNav(!showNav)}>
					{showNav ? <IoClose /> : <IoMenuOutline />}
				</div>
			</nav>
		</header>
	);
};

export default LandingNavbar;
