import React from "react";
import Link from "next/link";
import LandingLogo from "../landing_logo/LandingLogo";
import { FaGithub, FaLinkedin, FaDiscord, FaFacebookF, FaInstagram } from "react-icons/fa";

const LandingFooter = () => {
	return (
		<footer className="landing-footer">
			<div className="landing-footer-container wrapper">
				<div className="landing-footer-column">
					<LandingLogo />
					<p>
						Lorem ipsum dolor sit amet consectetur adipisicing elit. Corporis pariatur
						accusamus in sed tenetur illum, beatae nesciunt magnam distinctio natus
						dicta eos, labore aperiam alias, nobis iure quibusdam placeat nemo?
					</p>
				</div>
				<div className="landing-footer-column">
					<h3>Lorem ipsum</h3>
					<Link href="#">A</Link>
					<Link href="#">B</Link>
					<Link href="#">C</Link>
					<Link href="#">D</Link>
					<Link href="#">E</Link>
				</div>
				<div className="landing-footer-column">
					<h3>Socials</h3>
					<div className="landing-footer-icons">
						<Link href="#" className="landing-footer-icon">
							<FaGithub />
						</Link>
						<Link href="#" className="landing-footer-icon">
							<FaLinkedin />
						</Link>
						<Link href="#" className="landing-footer-icon">
							<FaDiscord />
						</Link>
						<Link href="#" className="landing-footer-icon">
							<FaFacebookF />
						</Link>
						<Link href="#" className="landing-footer-icon">
							<FaInstagram />
						</Link>
					</div>
				</div>
			</div>
		</footer>
	);
};

export default LandingFooter;
