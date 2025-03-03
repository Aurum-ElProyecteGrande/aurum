import React from 'react'

const ProfilePassword = ({ isEditingPassword, setIsEditingPassword, handlePasswordInput, handlePasswordChange, error }) => {
  return (
    <div className={`user-profile-password${isEditingPassword ? "" : " inactive"}`}>

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
      {error ? <p className="error-message">{error}</p> : <button
        className="primary-button"
        onClick={() => {
          if (isEditingPassword)
            handlePasswordChange();

          setIsEditingPassword(!isEditingPassword);
        }}
      >
        {isEditingPassword ? "Save" : "Change Password"}
      </button>}


    </div>
  )
}

export default ProfilePassword