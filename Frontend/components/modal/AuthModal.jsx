import React, { useState } from 'react';
import { IoMdClose } from "react-icons/io";

const AuthModal = ({ showModal, setShowModal, isSignUp, handleModalRoute }) => {
  const [isSignUpMode, setIsSignUpMode] = useState(isSignUp);
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;

    if (name == "confirmPassword") {
      if (value != formData.password)
        setError("Passwords do not match.");
      else
        setError("");
    }

    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    const endpoint = isSignUpMode ? '/api/user/register' : '/api/user/login';
    const payload = isSignUpMode
      ? {
        name: formData.name,
        email: formData.email,
        password: formData.password,
      }
      : {
        email: formData.email,
        password: formData.password,
      };

    try {
      const response = await fetch(endpoint, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok)
        throw new Error('Failed to authenticate.');

    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
      setShowModal(false)
      handleModalRoute(isSignUp)
    }
  };

  return (
    <div className={`auth-modal ${showModal ? 'visible' : ''}`}>
      <div className="auth-container">
        <div className={`form-section`}>
          <form onSubmit={handleSubmit}>
            <h1>{isSignUpMode ? 'Create Account' : 'Sign In'}</h1>
            <span>{isSignUpMode ? 'Use your email for registration' : 'Use your existing account'}</span>
            {isSignUpMode && (
              <input
                type="text"
                name="name"
                placeholder="Name"
                value={formData.name}
                onChange={handleChange}
                required
              />
            )}
            <input
              type="email"
              name="email"
              placeholder="Email"
              value={formData.email}
              onChange={handleChange}
              required
            />
            <input
              type="password"
              name="password"
              placeholder="Password"
              value={formData.password}
              onChange={handleChange}
              required
            />
            {isSignUpMode && (
              <input
                type="password"
                name="confirmPassword"
                placeholder="Confirm Password"
                value={formData.confirmPassword}
                onChange={handleChange}
                required
              />
            )}
            {error ? <p className="error-message">{error}</p> :
              <button className="primary-button" type="submit" disabled={loading}>
                {loading ? 'Loading...' : isSignUpMode ? 'Sign Up' : 'Sign In'}
              </button>}
          </form>
        </div>
        <div className="overlay-section">
          <span className="exit" onClick={() => setShowModal(false)}>
            <IoMdClose />
          </span>
          <div className="overlay-panel">
            <h1>{isSignUpMode ? 'Welcome Back!' : 'Hello, Friend!'}</h1>
            <p>{isSignUpMode
              ? 'Login with your personal info to stay connected.'
              : 'Enter your details to begin your journey with us.'}
            </p>
            <button
              className="accent-button"
              onClick={() => setIsSignUpMode(!isSignUpMode)}
            >
              {isSignUpMode ? 'Sign In' : 'Sign Up'}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AuthModal;
