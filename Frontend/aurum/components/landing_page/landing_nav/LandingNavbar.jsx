"use client";
import React, { useState, useEffect } from "react";
import Link from "next/link";
import LandingLogo from "../landing_logo/LandingLogo";
import { IoMenuOutline, IoClose } from "react-icons/io5";
import { fetchTest } from "@/scripts/landing_page_scripts/landing_page";

const LandingNavbar = () => {
	const [showNav, setShowNav] = useState(false);


	useEffect(() => {
		const fetchData = async () => {
			const data = await fetchTest();
			console.log(data);
		};

		fetchData();
	}, []);


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
				</ul>
				<div className="landing-navbar-buttons">
					<button className="transparent-button">Login</button>
					<button className="accent-button">Register</button>
				</div>
				<div className="landing-navbar-menu" onClick={() => setShowNav(!showNav)}>
					{showNav ? <IoClose /> : <IoMenuOutline />}
				</div>
			</nav>
		</header>
	);
};

export default LandingNavbar;
