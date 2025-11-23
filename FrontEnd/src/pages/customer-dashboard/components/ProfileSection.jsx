import React, { useState } from 'react';
        import Button from '../../../components/ui/Button';
        import Input from '../../../components/ui/Input';
        import Icon from '../../../components/AppIcon';
        import { updateUser } from '../../../utils/api';

        const ProfileSection = ({ userProfile, onUpdate }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editedProfile, setEditedProfile] = useState(userProfile);
  const [isUploading, setIsUploading] = useState(false);

  const handleInputChange = (field, value) => {
    setEditedProfile(prev => ({ ...prev, [field]: value }));
  };

  const handleSave = async () => {
    try {
      console.log('Updating user with:', editedProfile);
      await updateUser(editedProfile.userId, editedProfile);
      onUpdate(editedProfile);
      setIsEditing(false);
    } catch (err) {
      console.error('Update user error:', err);
      alert('Failed to update profile.');
    }
  };

          const handleCancel = () => {
            setEditedProfile(userProfile);
            setIsEditing(false);
          };

          const handleImageUpload = (event) => {
            const file = event.target.files?.[0];
            if (file) {
              setIsUploading(true);
              // Simulate image upload
              setTimeout(() => {
                const imageUrl = URL.createObjectURL(file);
                handleInputChange('profilePicture', imageUrl);
                setIsUploading(false);
              }, 1500);
            }
          };

          return (
            <div className="space-y-6">
              {/* Profile Header */}
              <div className="bg-card border border-border rounded-lg p-6">
                <div className="flex items-center justify-between mb-6">
                  <h2 className="text-xl font-semibold text-foreground">Profile Information</h2>
                  {!isEditing ? (
                    <Button
                      variant="outline"
                      onClick={() => setIsEditing(true)}
                      iconName="Edit"
                      iconPosition="left"
                    >
                      Edit Profile
                    </Button>
                  ) : (
                    <div className="flex space-x-2">
                      <Button
                        variant="outline"
                        onClick={handleCancel}
                        size="sm"
                      >
                        Cancel
                      </Button>
                      <Button
                        onClick={handleSave}
                        size="sm"
                        iconName="Save"
                        iconPosition="left"
                      >
                        Save Changes
                      </Button>
                    </div>
                  )}
                </div>

                {/* Profile Picture Section */}
                <div className="flex items-center space-x-6 mb-6">
                  <div className="relative">
                    <div className="w-24 h-24 rounded-full overflow-hidden bg-muted">
                      {isUploading ? (
                        <div className="w-full h-full flex items-center justify-center">
                          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
                        </div>
                      ) : (
                        <img
                          src={editedProfile?.profilePicture || '/default-profile.png'}
                          alt={editedProfile?.fullName || 'Profile'}
                          className="w-full h-full object-cover"
                        />
                      )}
                    </div>
                    {isEditing && (
                      <label className="absolute bottom-0 right-0 bg-primary text-primary-foreground rounded-full p-2 cursor-pointer hover:bg-primary/90 transition-colors">
                        <Icon name="Camera" size={16} />
                        <input
                          type="file"
                          accept="image/*"
                          onChange={e => {
                            const file = e.target.files?.[0];
                            if (file) {
                              setIsUploading(true);
                              const reader = new FileReader();
                              reader.onloadend = () => {
                                handleInputChange('profilePicture', reader.result);
                                setIsUploading(false);
                              };
                              reader.readAsDataURL(file);
                            }
                          }}
                          className="hidden"
                        />
                      </label>
                    )}
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold text-foreground">{editedProfile?.fullName}</h3>
                    <p className="text-muted-foreground">{editedProfile?.email}</p>
                    <p className="text-sm text-muted-foreground mt-1">
                      {editedProfile?.joinDate ? `Member since ${new Date(editedProfile?.joinDate).toLocaleDateString()}` : ''}
                    </p>
                  </div>
                </div>

                {/* Profile Form */}
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-sm font-medium text-foreground mb-2">
                    Full Name
                  </label>
                  <Input
                    value={editedProfile?.fullName || ''}
                    onChange={(e) => handleInputChange('fullName', e.target.value)}
                    disabled={!isEditing}
                    placeholder="Enter your full name"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-foreground mb-2">
                    Email Address
                  </label>
                  <Input
                    type="email"
                    value={editedProfile?.email || ''}
                    onChange={(e) => handleInputChange('email', e.target.value)}
                    disabled={!isEditing}
                    placeholder="Enter your email"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-foreground mb-2">
                    Phone Number
                  </label>
                  <Input
                    value={editedProfile?.phoneNumber || ''}
                    onChange={(e) => handleInputChange('phoneNumber', e.target.value)}
                    disabled={!isEditing}
                    placeholder="Enter your phone number"
                  />
                </div>
              </div>
              </div>

              {/* Address Section */}
              <div className="bg-card border border-border rounded-lg p-6">
                <h3 className="text-lg font-semibold text-foreground mb-4">Address Information</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                  <div className="md:col-span-2">
                    <label className="block text-sm font-medium text-foreground mb-2">
                      Street Address
                    </label>
                    <Input
                      value={editedProfile?.street || ''}
                      onChange={(e) => handleInputChange('street', e.target.value)}
                      disabled={!isEditing}
                      placeholder="Enter your street address"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-foreground mb-2">
                      City
                    </label>
                    <Input
                      value={editedProfile?.city || ''}
                      onChange={(e) => handleInputChange('city', e.target.value)}
                      disabled={!isEditing}
                      placeholder="Enter your city"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-foreground mb-2">
                      State
                    </label>
                    <Input
                      value={editedProfile?.state || ''}
                      onChange={(e) => handleInputChange('state', e.target.value)}
                      disabled={!isEditing}
                      placeholder="Enter your state"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-foreground mb-2">
                      ZIP Code
                    </label>
                    <Input
                      value={editedProfile?.zipCode || ''}
                      onChange={(e) => handleInputChange('zipCode', e.target.value)}
                      disabled={!isEditing}
                      placeholder="Enter your ZIP code"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-foreground mb-2">
                      Country
                    </label>
                    <Input
                      value={editedProfile?.country || ''}
                      onChange={(e) => handleInputChange('country', e.target.value)}
                      disabled={!isEditing}
                      placeholder="Enter your country"
                    />
                  </div>
                </div>
              </div>
            </div>
          );
        };

        export default ProfileSection;