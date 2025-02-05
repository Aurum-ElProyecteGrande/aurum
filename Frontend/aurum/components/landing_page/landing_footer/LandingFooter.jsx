import React from "react";
import Link from "next/link";
import LandingLogo from "../landing_logo/LandingLogo";
import { FaGithub, FaLinkedin, FaDiscord, FaFacebookF, FaInstagram } from "react-icons/fa";

const LandingFooter = () => {
	return (
		<footer id="footer" className="landing-footer">
			<div className="landing-footer-container wrapper">
				<div className="landing-footer-column">
					<LandingLogo />
					<p>
						Simplifying the way you manage your money, one expense at a time. Join us and take control of your finances.
					</p>
				</div>
				<div className="landing-footer-column">
					<h3>Site map</h3>
					<div className="site-map">
						<Link href="#prices">Prices</Link>
						<Link href="#views">Views</Link>
						<Link href="#footer">Socials</Link>
					</div>
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
