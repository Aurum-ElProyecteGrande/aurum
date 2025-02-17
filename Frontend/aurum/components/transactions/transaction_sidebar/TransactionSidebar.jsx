import React from 'react'
import Link from 'next/link'
import { MdLogout, MdFormatListBulleted } from "react-icons/md";
import { LuLayoutDashboard } from "react-icons/lu";
import Image from 'next/image'
import Logo from '@/imgs/aurum_logo.png'


function TransactionSidebar() {
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
                        <Link href="/">
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