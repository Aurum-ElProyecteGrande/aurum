import React from 'react'

const ProfileUsername = ({isEditingUsername, setIsEditingUsername, username, setUsername, handleSave}) => {
  return (
    <div className={`user-profile-username${isEditingUsername ? "" : " inactive"}`}>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <button
        className="primary-button"
        onClick={() => {
          if (isEditingUsername)
            handleSave();

          setIsEditingUsername(!isEditingUsername);
        }}
      >
        {isEditingUsername ? "Save" : "Change Username"}
      </button>
      </div>
  )
}

export default ProfileUsername