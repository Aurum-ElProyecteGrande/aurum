import React from 'react'

const ProfilePassword = ({isEditingPassword, setIsEditingPassword, handlePasswordInput, handlePasswordChange}) => {
  return (
    <div className="user-profile-password">
        {isEditingPassword ? (
          <div className="user-profile-password-edit">
            <input
              type="password"
              name="oldPassword"
              placeholder="Old Password"
              onChange={handlePasswordInput}
            />
            <input
              type="password"
              name="newPassword"
              placeholder="New Password"
              onChange={handlePasswordInput}
            />
            <input
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              onChange={handlePasswordInput}
            />
            <button
              onClick={() => {
                handlePasswordChange();
                setIsEditingPassword(false);
              }}
            >
              Save
            </button>
          </div>
        ) : (
          <button
            onClick={() => setIsEditingPassword(true)}
          >
            Change Password
          </button>
        )}
      </div>
  )
}

export default ProfilePassword