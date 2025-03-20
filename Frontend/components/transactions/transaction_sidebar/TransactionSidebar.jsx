import React from 'react'
import Link from 'next/link'
import { MdLogout, MdFormatListBulleted, MdOutlineAccountBalance } from "react-icons/md";
import { LuLayoutDashboard } from "react-icons/lu";
import { CgProfile } from "react-icons/cg";
import Image from 'next/image'
import Logo from '@/imgs/aurum_logo.png'
import { fetchLogout } from '@/scripts/landing_page_scripts/landing_page';

function TransactionSidebar() {
    const handleLogOut = async () => {
        await fetchLogout()
    }

    return (
        <aside className='transactions-sidebar'>
            <div className="transactions-sidebar-container">
                <div className="transactions-sidebar-logo">
                    <Image
                        src={Logo}
                        alt="Aurum logo"
                        width={151}
                        height={153}
                    />
                </div>
                <ul className="transactions-sidebar-menu">
                    <li>
                        <Link href="/dashboard">
                            <div className='transactions-sidebar-menu-item'>
                                <LuLayoutDashboard />
                                <p>Dashboard</p>
                            </div>
                        </Link>
                    </li>
                    <li>
                        <Link href="/transactions">
                            <div className='transactions-sidebar-menu-item'>
                                <MdFormatListBulleted />
                                <p>Transactions</p>
                            </div>
                        </Link>
                    </li>
                    <li>
                        <Link href="/accounts">
                            <div className='transactions-sidebar-menu-item'>
                                <MdOutlineAccountBalance />
                                <p>Accounts</p>
                            </div>
                        </Link>
                    </li>
                    <li>
                        <Link href="/profile">
                            <div className='transactions-sidebar-menu-item'>
                                <CgProfile />
                                <p>Profile</p>
                            </div>
                        </Link>
                    </li>
                    <li>
                        <Link href="/" onClick={handleLogOut}>
                            <div className='transactions-sidebar-menu-item'>
                                <MdLogout />
                                <p>Logout</p>
                            </div>
                        </Link>
                    </li>
                </ul>
            </div>
        </aside>
    )
}

export default TransactionSidebar