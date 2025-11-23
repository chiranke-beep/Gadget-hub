import React, { useState } from 'react';
import Button from '../../../components/ui/Button';
import Input from '../../../components/ui/Input';

const AccountSettings = ({ userProfile, onUpdate }) => {
  const [passwords, setPasswords] = useState({
    currentPassword: '',
    newPassword: '',
    confirmPassword: ''
  });

  const [isChangingPassword, setIsChangingPassword] = useState(false);

  const handlePasswordChange = (field, value) => {
    setPasswords(prev => ({ ...prev, [field]: value }));
  };

  const handlePasswordSubmit = async (e) => {
    e.preventDefault();
    if (passwords.newPassword !== passwords.confirmPassword) {
      alert('New passwords do not match');
      return;
    }
    if (passwords.newPassword.length < 8) {
      alert('Password must be at least 8 characters long');
      return;
    }

    setIsChangingPassword(true);
    // Simulate API call
    setTimeout(() => {
      setIsChangingPassword(false);
      setPasswords({
        currentPassword: '',
        newPassword: '',
        confirmPassword: ''
      });
      console.log('Password changed successfully');
    }, 1500);
  };

  return (
    <div className="space-y-6">
      {/* Change Password */}
      <div className="bg-card border border-border rounded-lg p-6">
        <h3 className="text-lg font-semibold text-foreground mb-4">Change Password</h3>
        <form onSubmit={handlePasswordSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-foreground mb-2">
              Current Password
            </label>
            <Input
              type="password"
              value={passwords.currentPassword}
              onChange={(e) => handlePasswordChange('currentPassword', e.target.value)}
              placeholder="Enter current password"
              required
            />
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-foreground mb-2">
                New Password
              </label>
              <Input
                type="password"
                value={passwords.newPassword}
                onChange={(e) => handlePasswordChange('newPassword', e.target.value)}
                placeholder="Enter new password"
                minLength={8}
                required
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-foreground mb-2">
                Confirm New Password
              </label>
              <Input
                type="password"
                value={passwords.confirmPassword}
                onChange={(e) => handlePasswordChange('confirmPassword', e.target.value)}
                placeholder="Confirm new password"
                minLength={8}
                required
              />
            </div>
          </div>
          <Button
            type="submit"
            loading={isChangingPassword}
            iconName="Lock"
            iconPosition="left"
          >
            Change Password
          </Button>
        </form>
      </div>

      {/* Account Actions */}
      <div className="bg-card border border-border rounded-lg p-6">
        <h3 className="text-lg font-semibold text-foreground mb-4">Account Actions</h3>
        <div className="pt-4 space-y-4">
          <Button
            variant="outline"
            className="border-destructive text-destructive hover:bg-destructive/10"
            iconName="LogOut"
            iconPosition="left"
            onClick={() => {
              localStorage.removeItem('user');
              localStorage.removeItem('authToken');
              localStorage.removeItem('userEmail');
              localStorage.removeItem('userName');
              localStorage.removeItem('isAuthenticated');
              window.location.href = '/login-screen';
            }}
          >
            Log Out
          </Button>
          <h4 className="text-base font-medium text-error mb-2">Danger Zone</h4>
          <p className="text-sm text-muted-foreground mb-4">
            This action is permanent and cannot be undone.
          </p>
          <Button
            variant="outline" 
            className="border-error text-error hover:bg-error/10"
            iconName="Trash2"
            iconPosition="left"
          >
            Delete Account
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AccountSettings;