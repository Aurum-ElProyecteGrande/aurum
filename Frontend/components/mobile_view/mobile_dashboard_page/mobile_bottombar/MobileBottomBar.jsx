import React, { useState } from 'react';
import { MdLogout, MdFormatListBulleted } from "react-icons/md";
import { LuLayoutDashboard } from "react-icons/lu";
import { CgProfile } from "react-icons/cg";
import { fetchLogout } from '@/scripts/landing_page_scripts/landing_page';
import Link from 'next/link';

const MobileBottomBar = ({ handleItemClick, activeItem}) => {
    const handleLogOut = async () => {
        await fetchLogout()
    }

    return (
        <div className="bottom-bar">
            <div className="bottom-bar-container">
                <ul className="bottom-bar-menu">
                    <li className={activeItem === "dashboard" ? "active" : ""} onClick={() => handleItemClick("dashboard")}>
                        <Link href="/dashboard">
                            <LuLayoutDashboard />
                        </Link>
                    </li>
                    <li className={activeItem === "transactions" ? "active" : ""} onClick={() => handleItemClick("transactions")}>
                        <Link href="/transactions">
                            <MdFormatListBulleted />
                        </Link>
                    </li>
                    <li className={activeItem === "profile" ? "active" : ""} onClick={() => handleItemClick("profile")}>
                        <Link href="/profile">
                            <CgProfile />
                        </Link>
                    </li>
                    <li>
                        <Link href="/" onClick={handleLogOut}>
                            <MdLogout />
                        </Link>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default MobileBottomBar;
