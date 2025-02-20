import React from 'react'

const ProfileUsername = ({isEditingUsername, setIsEditingUsername, username, setUsername, handleSave}) => {
  return (
    <div className="user-profile-username">
        {isEditingUsername ? (
          <div className="user-profile-username-edit">
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <button
              onClick={() => {
                handleSave();
                setIsEditingUsername(false);
              }}
            >
              Save
            </button>
          </div>
        ) : (
          <div className="user-profile-username-display">
            <p>{username}</p>
            <button
              onClick={() => setIsEditingUsername(true)}
            >
              Edit Username
            </button>
          </div>
        )}
      </div>
  )
}

export default ProfileUsername