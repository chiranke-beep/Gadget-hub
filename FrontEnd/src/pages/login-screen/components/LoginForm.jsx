import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { motion } from 'framer-motion';
import Button from '../../../components/ui/Button';
import Input from '../../../components/ui/Input';
import { Checkbox } from '../../../components/ui/Checkbox';
import Icon from '../../../components/AppIcon';

const LoginForm = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    rememberMe: false
  });
  const [errors, setErrors] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);


  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
    
    // Clear error when user starts typing
    if (errors[name]) {
      setErrors(prev => ({
        ...prev,
        [name]: ''
      }));
    }
  };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'Please enter a valid email address';
    }

    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) return;

    setIsLoading(true);
    try {
      const response = await fetch('http://localhost:5254/api/User', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
      });
      if (!response.ok) throw new Error('Failed to connect to server');
      const users = await response.json();
      const found = users.find(
        u => u.email === formData.email && atob(u.passwordHash) === formData.password
      );
      if (found) {
        localStorage.setItem('isAuthenticated', 'true');
        localStorage.setItem('userEmail', found.email);
        localStorage.setItem('userName', found.userName);
        // Save user object for header authentication
        localStorage.setItem('user', JSON.stringify(found));
        if (formData.rememberMe) {
          localStorage.setItem('rememberMe', 'true');
        }
        navigate('/home-page');
      } else {
        setErrors({ general: 'Invalid email or password. Please try again.' });
      }
    } catch (error) {
      setErrors({ general: error.message || 'Login failed. Please try again.' });
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.5 }}
      className="w-full max-w-md mx-auto"
    >
      <div className="bg-card border border-border rounded-xl p-8 shadow-lg">
        <div className="text-center mb-8">
          <motion.div
            initial={{ scale: 0.8 }}
            animate={{ scale: 1 }}
            transition={{ delay: 0.2, duration: 0.3 }}
          >
            <h1 className="text-3xl font-bold text-foreground mb-2">Welcome Back</h1>
            <p className="text-muted-foreground">Sign in to your GadgetHub account</p>
          </motion.div>
        </div>

        {errors.general && (
          <motion.div
            initial={{ opacity: 0, x: -10 }}
            animate={{ opacity: 1, x: 0 }}
            className="mb-6 p-4 bg-error/10 border border-error/20 rounded-lg flex items-center space-x-3"
          >
            <Icon name="AlertCircle" size={20} className="text-error flex-shrink-0" />
            <p className="text-error text-sm">{errors.general}</p>
          </motion.div>
        )}

        <form onSubmit={handleSubmit} className="space-y-6">
          <motion.div
            initial={{ opacity: 0, x: -20 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.3, duration: 0.3 }}
          >
            <Input
              label="Email Address"
              type="email"
              name="email"
              placeholder="Enter your email"
              value={formData.email}
              onChange={handleInputChange}
              error={errors.email}
              required
              className="transition-all duration-200"
            />
          </motion.div>

          <motion.div
            initial={{ opacity: 0, x: -20 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.4, duration: 0.3 }}
            className="relative"
          >
            <Input
              label="Password"
              type={showPassword ? "text" : "password"}
              name="password"
              placeholder="Enter your password"
              value={formData.password}
              onChange={handleInputChange}
              error={errors.password}
              required
              className="transition-all duration-200"
            />
            <button
              type="button"
              onClick={() => setShowPassword(!showPassword)}
              className="absolute right-3 top-9 text-muted-foreground hover:text-foreground transition-colors duration-150"
            >
              <Icon name={showPassword ? "EyeOff" : "Eye"} size={18} />
            </button>
          </motion.div>

          <motion.div
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.5, duration: 0.3 }}
            className="flex items-center justify-between"
          >
            <Checkbox
              label="Remember me"
              name="rememberMe"
              checked={formData.rememberMe}
              onChange={handleInputChange}
              className="text-sm"
            />
            
            <Link
              to="/forgot-password"
              className="text-sm text-primary hover:text-primary/80 transition-colors duration-150 font-medium"
            >
              Forgot password?
            </Link>
          </motion.div>

          <motion.div
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.6, duration: 0.3 }}
          >
            <Button
              type="submit"
              variant="default"
              loading={isLoading}
              fullWidth
              iconName={isLoading ? undefined : "LogIn"}
              iconPosition="left"
              className="h-12 text-base font-semibold"
            >
              {isLoading ? 'Signing In...' : 'Sign In'}
            </Button>
          </motion.div>
        </form>

        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.7, duration: 0.3 }}
          className="mt-8 text-center"
        >
          <p className="text-muted-foreground">
            Don't have an account?{' '}
            <Link
              to="/registration-screen"
              className="text-primary hover:text-primary/80 transition-colors duration-150 font-semibold"
            >
              Register here
            </Link>
          </p>
        </motion.div>

        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.8, duration: 0.3 }}
          className="mt-6 pt-6 border-t border-border"
        >
          <div className="text-center">
            <p className="text-xs text-muted-foreground mb-3">Or continue with</p>
            <div className="flex space-x-3">
              <Button
                variant="outline"
                className="flex-1 h-10"
                iconName="Mail"
                iconPosition="left"
                iconSize={16}
              >
                Google
              </Button>
              <Button
                variant="outline"
                className="flex-1 h-10"
                iconName="Github"
                iconPosition="left"
                iconSize={16}
              >
                GitHub
              </Button>
            </div>
          </div>
        </motion.div>
      </div>
    </motion.div>
  );
};

export default LoginForm;