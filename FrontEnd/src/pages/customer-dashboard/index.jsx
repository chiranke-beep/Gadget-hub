import React, { useState, useEffect } from 'react';
        import { useNavigate } from 'react-router-dom';
        import Header from '../../components/ui/Header';
        import ProfileSection from './components/ProfileSection';
        import OrderHistory from './components/OrderHistory';
        import QuotationRequests from './components/QuotationRequests';
        import AccountSettings from './components/AccountSettings';
        import DashboardSidebar from './components/DashboardSidebar';
        import Button from '../../components/ui/Button';
        import Icon from '../../components/AppIcon';
import { fetchUsers } from '../../utils/api';

        const CustomerDashboard = () => {
          const navigate = useNavigate();
          const [activeSection, setActiveSection] = useState('overview');
          const [isMobileSidebarOpen, setIsMobileSidebarOpen] = useState(false);
          const [userProfile, setUserProfile] = useState({});
          const [isLoading, setIsLoading] = useState(true);

          // Set section from hash on mount and when hash changes
          useEffect(() => {
            const setSectionFromHash = () => {
              const hash = window.location.hash.replace('#', '');
              if (hash && ['overview','profile','orders','quotations','settings'].includes(hash)) {
                setActiveSection(hash);
              }
            };
            setSectionFromHash();
            window.addEventListener('hashchange', setSectionFromHash);
            return () => window.removeEventListener('hashchange', setSectionFromHash);
          }, []);

          useEffect(() => {
            async function loadUser() {
              try {
                const users = await fetchUsers();
                // Get logged-in userId from localStorage
                let userId = null;
                try {
                  const user = JSON.parse(localStorage.getItem('user'));
                  userId = user?.userId;
                } catch {}
                let user = null;
                if (userId) {
                  user = users.find(u => u.userId === userId);
                }
                setUserProfile(user || users[0] || {});
              } catch (err) {
                setUserProfile({ name: 'Error loading user', email: '', accountStatus: 'N/A' });
              } finally {
                setIsLoading(false);
              }
            }
            loadUser();
          }, []);

          const dashboardSections = [
            { id: 'overview', label: 'Overview', icon: 'LayoutDashboard' },
            { id: 'profile', label: 'Profile Information', icon: 'User' },
            { id: 'orders', label: 'Order History', icon: 'Package' },
            { id: 'quotations', label: 'Quotation Requests', icon: 'FileText' },
            { id: 'settings', label: 'Account Settings', icon: 'Settings' }
          ];

          const handleSectionChange = (sectionId) => {
            setActiveSection(sectionId);
            setIsMobileSidebarOpen(false);
          };

          const handleProfileUpdate = (updatedProfile) => {
            setUserProfile(prev => ({ ...prev, ...updatedProfile }));
          };

          if (isLoading) {
            return (
              <div className="min-h-screen bg-background">
                <Header />
                <div className="pt-20 pb-12">
                  <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                    <div className="flex items-center justify-center min-h-[400px]">
                      <div className="flex items-center space-x-3">
                        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
                        <span className="text-muted-foreground">Loading dashboard...</span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            );
          }

          const renderActiveSection = () => {
            switch (activeSection) {
              case 'overview':
                return (
                  <div className="space-y-6">
                    {/* Welcome Section */}
                    <div className="bg-card border border-border rounded-lg p-6">
                      <div className="flex items-center space-x-4">
                        <div className="w-16 h-16 rounded-full overflow-hidden bg-muted">
                          <img
                            src={userProfile?.profilePicture}
                            alt={userProfile?.name}
                            className="w-full h-full object-cover"
                          />
                        </div>
                        <div>
                          <h2 className="text-2xl font-bold text-foreground">
                            Welcome back, {userProfile?.name}!
                          </h2>
                          <p className="text-muted-foreground">
                            Account Status: <span className="text-accent font-medium">{userProfile?.accountStatus}</span>
                          </p>
                        </div>
                      </div>
                    </div>

                    {/* Quick Stats */}
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                      <div className="bg-card border border-border rounded-lg p-6">
                        <div className="flex items-center space-x-3">
                          <div className="w-12 h-12 bg-primary/10 rounded-lg flex items-center justify-center">
                            <Icon name="Package" size={24} className="text-primary" />
                          </div>
                          <div>
                            <p className="text-2xl font-bold text-foreground">{userProfile?.totalOrders}</p>
                            <p className="text-sm text-muted-foreground">Total Orders</p>
                          </div>
                        </div>
                      </div>
                      <div className="bg-card border border-border rounded-lg p-6">
                        <div className="flex items-center space-x-3">
                          <div className="w-12 h-12 bg-warning/10 rounded-lg flex items-center justify-center">
                            <Icon name="FileText" size={24} className="text-warning" />
                          </div>
                          <div>
                            <p className="text-2xl font-bold text-foreground">{userProfile?.pendingQuotations}</p>
                            <p className="text-sm text-muted-foreground">Pending Quotations</p>
                          </div>
                        </div>
                      </div>
                    </div>

                    {/* Quick Actions */}
                    <div className="bg-card border border-border rounded-lg p-6">
                      <h3 className="text-lg font-semibold text-foreground mb-4">Quick Actions</h3>
                      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                        <Button
                          variant="outline"
                          onClick={() => navigate('/product-catalog')}
                          iconName="ShoppingBag"
                          iconPosition="left"
                          className="justify-start"
                        >
                          Browse Products
                        </Button>
                        <Button
                          variant="outline"
                          onClick={() => handleSectionChange('orders')}
                          iconName="Package"
                          iconPosition="left"
                          className="justify-start"
                        >
                          View Orders
                        </Button>
                        <Button
                          variant="outline"
                          onClick={() => handleSectionChange('quotations')}
                          iconName="FileText"
                          iconPosition="left"
                          className="justify-start"
                        >
                          Request Quotation
                        </Button>
                      </div>
                    </div>
                  </div>
                );
              case 'profile':
                return <ProfileSection userProfile={userProfile} onUpdate={handleProfileUpdate} />;
              case 'orders':
                return <OrderHistory />;
              case 'quotations':
                return <QuotationRequests />;
              case 'settings':
                return <AccountSettings userProfile={userProfile} onUpdate={handleProfileUpdate} />;
              default:
                return <div>Section not found</div>;
            }
          };

          return (
            <div className="min-h-screen bg-background">
              <Header />
              
              <div className="pt-20 pb-12">
                <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                  {/* Mobile Header */}
                  <div className="flex items-center justify-between mb-6 lg:hidden">
                    <h1 className="text-2xl font-bold text-foreground">Dashboard</h1>
                    <Button
                      variant="outline"
                      onClick={() => setIsMobileSidebarOpen(!isMobileSidebarOpen)}
                      iconName="Menu"
                      className="lg:hidden"
                    />
                  </div>

                  {/* Desktop Header */}
                  <div className="hidden lg:block mb-8">
                    <h1 className="text-3xl font-bold text-foreground">Customer Dashboard</h1>
                    <p className="text-muted-foreground mt-2">
                      Manage your account, orders, and preferences
                    </p>
                  </div>

                  <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">
                    {/* Sidebar */}
                    <div className={`lg:col-span-1 ${isMobileSidebarOpen ? 'block' : 'hidden lg:block'}`}>
                      <DashboardSidebar
                        sections={dashboardSections}
                        activeSection={activeSection}
                        onSectionChange={handleSectionChange}
                      />
                    </div>

                    {/* Main Content */}
                    <div className="lg:col-span-3">
                      {renderActiveSection()}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          );
        };

        export default CustomerDashboard;