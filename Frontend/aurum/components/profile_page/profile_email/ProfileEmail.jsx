import React from 'react'

const ProfileEmail = ({email, setEmail, isEditingEmail ,setIsEditingEmail, handleSave}) => {
  return (
    <div className="user-profile-email">
        {isEditingEmail ? (
          <div className="user-profile-email-edit">
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <button
              onClick={() => {
                handleSave();
                setIsEditingEmail(false);
              }}
            >
              Save
            </button>
          </div>
        ) : (
          <div className="user-profile-email-display">
            <p>{email}</p>
            <button
              onClick={() => setIsEditingEmail(true)}
            >
              Edit Email
            </button>
          </div>
        )}
      </div>
  )
}

export default ProfileEmail