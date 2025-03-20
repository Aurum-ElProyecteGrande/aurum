import React, { useState } from 'react';
import { MdLogout, MdFormatListBulleted } from "react-icons/md";
import { LuLayoutDashboard, LuCalendarRange } from "react-icons/lu";
import { CgProfile } from "react-icons/cg";
import { fetchLogout } from '@/scripts/landing_page_scripts/landing_page';
import Link from 'next/link';

const MobileBottomBar = ({ }) => {
    const handleLogOut = async () => {
        await fetchLogout()
    }

    return (
        <div className="bottom-bar">
            <div className="bottom-bar-container">
                <ul className="bottom-bar-menu">
                    <li>
                        <Link href="/dashboard">
                            <LuLayoutDashboard />
                        </Link>
                    </li>
                    <li>
                        <Link href="/transactions">
                            <MdFormatListBulleted />
                        </Link>
                    </li>
                    <li>
                        <Link href="/regulars">
                                <LuCalendarRange />
                        </Link>
                    </li>
                    <li>
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
