import React from 'react'

const ProfilePic = ({profilePicture, handleProfilePictureChange}) => {
  return (
    <div className="user-profile-picture">
        <img
          src={
            profilePicture || "https://via.placeholder.com/150"
          }
          alt="Profile"
        />
        <label htmlFor="profilePicture">
          Change Profile Picture
        </label>
        <input
          type="file"
          id="profilePicture"
          onChange={handleProfilePictureChange}
        />
      </div>
  )
}

export default ProfilePic