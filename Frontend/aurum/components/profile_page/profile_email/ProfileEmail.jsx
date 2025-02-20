import React from 'react'

const ProfileEmail = ({ email, setEmail, isEditingEmail, setIsEditingEmail, handleSave }) => {
  return (
    <div className={`user-profile-email${isEditingEmail ? "" : " inactive"}`}>

      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <button
        className="primary-button"
        onClick={() => {
          if (isEditingEmail)
            handleSave();

          setIsEditingEmail(!isEditingEmail);
        }}
      >
        {isEditingEmail ? "Save" : "Change Email"}
      </button>
    </div>
  )
}

export default ProfileEmail