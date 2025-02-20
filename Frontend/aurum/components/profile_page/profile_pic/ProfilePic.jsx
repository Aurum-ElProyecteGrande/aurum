import React from 'react'

const ProfilePic = ({profilePicture, handleProfilePictureChange}) => {
  return (
    <div className="user-profile-picture">
        <img
          src={
            profilePicture || "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTd1Z3I4enuWtfMxeCRHUpoDQWQtlf9XJMI1w&s"
          }
          alt="Profile"
        />
        <label htmlFor="profilePicture" onClick={() => alert("Paywall sorry")}>
          Change Profile Picture
        </label>
        {/* <input
          type="file"
          id="profilePicture"
          onChange={handleProfilePictureChange}
        /> */}
      </div>
  )
}

export default ProfilePic