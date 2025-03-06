"use client"
import React, { useState, useEffect } from "react";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import ProfilePic from "@/components/profile_page/profile_pic/ProfilePic";
import ProfileUsername from "@/components/profile_page/profile_username/ProfileUsername";
import ProfileEmail from "@/components/profile_page/profile_email/ProfileEmail";
import ProfilePassword from "@/components/profile_page/profile_password/ProfilePassword";
import { fetchUserInfo, fetchUserInfoChange, fetchPasswordChange } from "@/scripts/profile_scripts/profile_scripts";
import useDeviceDetect from "@/hook/useDeviceDetect";
import MobileBottomBar from "@/components/mobile_view/mobile_dashboard_page/mobile_bottombar/MobileBottomBar";

const ProfilePage = () => {
    const [isEditingUsername, setIsEditingUsername] = useState(false);
    const [isEditingEmail, setIsEditingEmail] = useState(false);
    const [isEditingPassword, setIsEditingPassword] = useState(false);
    const [username, setUsername] = useState("JohnDoe");
    const [email, setEmail] = useState("john.doe@example.com");
    const [password, setPassword] = useState({})
    const [profilePicture, setProfilePicture] = useState(null);
    const [error, setError] = useState('');
    const isTabletPortrait = useDeviceDetect();

    const handleProfilePictureChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            setProfilePicture(URL.createObjectURL(file));
        }
    };

    const handlePasswordInput = (e) => {
        const { name, value } = e.target;

        if (name == "confirmPassword") {
            if (value != password.newPassword)
              setError("Passwords do not match.");
            else
              setError("");
          }

        setPassword((prevValues) => ({
            ...prevValues,
            [name]: value, e
        }));
    }

    const handleSave = async () => {
        const body = {
            Email: email,
            UserName: username
        }

        const isSuccess = await fetchUserInfoChange(body)

        if (!isSuccess)
            console.error("Problem")
        else
            console.log("No problem")

    };

    const handlePasswordChange = async () => {
        const body = {
            OldPassword: password.oldPassword,
            NewPassword: password.newPassword
        }

        const isSuccess = await fetchPasswordChange(body)

        if (!isSuccess)
            console.error("Problem")
        else
            console.log("No problem")

    }

    const GetUserInfo = async () => {
        const userInfo = await fetchUserInfo();
        setEmail(userInfo.email)
        setUsername(userInfo.userName)
    }

    useEffect(() => {
        GetUserInfo();
    }, [])


    return (
        <section className="user-profile">
            {isTabletPortrait ? <MobileBottomBar /> : <TransactionSidebar />}
            <div className="user-profile-container wrapper">
                <ProfilePic profilePicture={profilePicture} handleProfilePictureChange={handleProfilePictureChange} />
                <ProfileUsername isEditingUsername={isEditingUsername} setIsEditingUsername={setIsEditingUsername} username={username} setUsername={setUsername} handleSave={handleSave} />
                <ProfileEmail email={email} setEmail={setEmail} isEditingEmail={isEditingEmail} setIsEditingEmail={setIsEditingEmail} handleSave={handleSave} />
                <ProfilePassword isEditingPassword={isEditingPassword} setIsEditingPassword={setIsEditingPassword}
                    handlePasswordInput={handlePasswordInput}
                    handlePasswordChange={handlePasswordChange}
                    error={error} />   
            </div>
        </section>
    );
};

export default ProfilePage;
