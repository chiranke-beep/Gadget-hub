import React, { useState, useEffect } from 'react';
        import Button from '../../../components/ui/Button';
        import Input from '../../../components/ui/Input';
        import Icon from '../../../components/AppIcon';

        const QuotationRequests = () => {
          const [quotations, setQuotations] = useState([]);
          const [isLoading, setIsLoading] = useState(true);
          const [showNewRequestForm, setShowNewRequestForm] = useState(false);
          const [newRequest, setNewRequest] = useState({
            productName: '',
            quantity: '',
            specifications: '',
            urgency: 'normal'
          });

          // Mock quotations data
          const mockQuotations = [
            {
              id: 'QUO-2024-001',
              date: '2024-01-20',
              status: 'pending',
              productName: 'Custom Gaming PC Build',
              quantity: 2,
              specifications: 'RTX 4090, i9-13900K, 64GB RAM, Custom Loop Cooling',
              estimatedResponse: '2024-01-22',
              urgency: 'high',
              responses: []
            },
            {
              id: 'QUO-2024-002',
              date: '2024-01-18',
              status: 'responded',
              productName: 'Server Hardware Bundle',
              quantity: 5,
              specifications: 'Xeon processors, 128GB ECC RAM per unit, Enterprise SSDs',
              estimatedResponse: '2024-01-20',
              urgency: 'normal',
              responses: [
                {
                  vendor: 'TechSupply Co.',
                  price: 15999.99,
                  delivery: '7-10 business days',
                  notes: 'Bulk discount applied, includes 3-year warranty'
                }
              ]
            },
            {
              id: 'QUO-2024-003',
              date: '2024-01-15',
              status: 'approved',
              productName: 'Network Infrastructure Equipment',
              quantity: 1,
              specifications: '48-port managed switch, enterprise firewall, wireless access points',
              estimatedResponse: '2024-01-17',
              urgency: 'low',
              responses: [
                {
                  vendor: 'NetworkPro Solutions',
                  price: 8499.99,
                  delivery: '5-7 business days',
                  notes: 'Installation and configuration included'
                }
              ],
              approvedOn: '2024-01-19'
            }
          ];

          useEffect(() => {
            // Simulate API call
            const timer = setTimeout(() => {
              setQuotations(mockQuotations);
              setIsLoading(false);
            }, 1000);

            return () => clearTimeout(timer);
          }, []);

          const getStatusColor = (status) => {
            switch (status) {
              case 'approved':
                return 'text-success bg-success/10 border-success/20';
              case 'responded':
                return 'text-primary bg-primary/10 border-primary/20';
              case 'pending':
                return 'text-warning bg-warning/10 border-warning/20';
              case 'declined':
                return 'text-error bg-error/10 border-error/20';
              default:
                return 'text-muted-foreground bg-muted border-border';
            }
          };

          const getStatusIcon = (status) => {
            switch (status) {
              case 'approved':
                return 'CheckCircle';
              case 'responded':
                return 'MessageSquare';
              case 'pending':
                return 'Clock';
              case 'declined':
                return 'XCircle';
              default:
                return 'FileText';
            }
          };

          const getUrgencyColor = (urgency) => {
            switch (urgency) {
              case 'high':
                return 'text-error';
              case 'normal':
                return 'text-warning';
              case 'low':
                return 'text-success';
              default:
                return 'text-muted-foreground';
            }
          };

          const handleNewRequestSubmit = (e) => {
            e.preventDefault();
            if (!newRequest.productName || !newRequest.quantity) return;

            const newQuotation = {
              id: `QUO-2024-${String(quotations.length + 1).padStart(3, '0')}`,
              date: new Date().toISOString().split('T')[0],
              status: 'pending',
              ...newRequest,
              quantity: parseInt(newRequest.quantity),
              estimatedResponse: new Date(Date.now() + 2 * 24 * 60 * 60 * 1000).toISOString().split('T')[0],
              responses: []
            };

            setQuotations(prev => [newQuotation, ...prev]);
            setNewRequest({
              productName: '',
              quantity: '',
              specifications: '',
              urgency: 'normal'
            });
            setShowNewRequestForm(false);
          };

          if (isLoading) {
            return (
              <div className="space-y-4">
                <div className="bg-card border border-border rounded-lg p-6">
                  <div className="flex items-center justify-center min-h-[200px]">
                    <div className="flex items-center space-x-3">
                      <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary"></div>
                      <span className="text-muted-foreground">Loading quotations...</span>
                    </div>
                  </div>
                </div>
              </div>
            );
          }

          return (
            <div className="space-y-6">
              {/* Header */}
              <div className="bg-card border border-border rounded-lg p-6">
                <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
                  <div>
                    <h2 className="text-xl font-semibold text-foreground">Quotation Requests</h2>
                    <p className="text-muted-foreground">Request custom quotes for bulk orders or special requirements</p>
                  </div>
                  <Button
                    onClick={() => setShowNewRequestForm(!showNewRequestForm)}
                    iconName="Plus"
                    iconPosition="left"
                  >
                    New Request
                  </Button>
                </div>
              </div>

              {/* New Request Form */}
              {showNewRequestForm && (
                <div className="bg-card border border-border rounded-lg p-6">
                  <h3 className="text-lg font-semibold text-foreground mb-4">Submit New Quotation Request</h3>
                  <form onSubmit={handleNewRequestSubmit} className="space-y-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium text-foreground mb-2">
                          Product/Service Name *
                        </label>
                        <Input
                          value={newRequest.productName}
                          onChange={(e) => setNewRequest(prev => ({ ...prev, productName: e.target.value }))}
                          placeholder="Enter product or service name"
                          required
                        />
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-foreground mb-2">
                          Quantity *
                        </label>
                        <Input
                          type="number"
                          min="1"
                          value={newRequest.quantity}
                          onChange={(e) => setNewRequest(prev => ({ ...prev, quantity: e.target.value }))}
                          placeholder="Enter quantity"
                          required
                        />
                      </div>
                    </div>
                    
                    <div>
                      <label className="block text-sm font-medium text-foreground mb-2">
                        Specifications & Requirements
                      </label>
                      <textarea
                        value={newRequest.specifications}
                        onChange={(e) => setNewRequest(prev => ({ ...prev, specifications: e.target.value }))}
                        placeholder="Describe your specific requirements, configurations, or special needs..."
                        rows={4}
                        className="w-full px-3 py-2 border border-border rounded-lg bg-input text-foreground placeholder-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring focus:border-transparent"
                      />
                    </div>
                    
                    <div>
                      <label className="block text-sm font-medium text-foreground mb-2">
                        Urgency Level
                      </label>
                      <select
                        value={newRequest.urgency}
                        onChange={(e) => setNewRequest(prev => ({ ...prev, urgency: e.target.value }))}
                        className="w-full px-3 py-2 border border-border rounded-lg bg-input text-foreground focus:outline-none focus:ring-2 focus:ring-ring focus:border-transparent"
                      >
                        <option value="low">Low - Standard processing (5-7 days)</option>
                        <option value="normal">Normal - Priority processing (2-3 days)</option>
                        <option value="high">High - Urgent processing (24-48 hours)</option>
                      </select>
                    </div>
                    
                    <div className="flex space-x-3">
                      <Button type="submit" iconName="Send" iconPosition="left">
                        Submit Request
                      </Button>
                      <Button
                        type="button"
                        variant="outline"
                        onClick={() => setShowNewRequestForm(false)}
                      >
                        Cancel
                      </Button>
                    </div>
                  </form>
                </div>
              )}

              {/* Quotations List */}
              {quotations.length === 0 ? (
                <div className="bg-card border border-border rounded-lg p-12 text-center">
                  <Icon name="FileText" size={48} className="text-muted-foreground mx-auto mb-4" />
                  <h3 className="text-lg font-semibold text-foreground mb-2">No quotation requests</h3>
                  <p className="text-muted-foreground mb-6">
                    You haven't submitted any quotation requests yet
                  </p>
                  <Button
                    onClick={() => setShowNewRequestForm(true)}
                    iconName="Plus"
                    iconPosition="left"
                  >
                    Create First Request
                  </Button>
                </div>
              ) : (
                <div className="space-y-4">
                  {quotations.map((quotation) => (
                    <div key={quotation.id} className="bg-card border border-border rounded-lg p-6">
                      {/* Quotation Header */}
                      <div className="flex flex-col sm:flex-row sm:items-start sm:justify-between gap-4 mb-4">
                        <div className="flex-1">
                          <div className="flex items-center space-x-3 mb-2">
                            <h3 className="text-lg font-semibold text-foreground">
                              {quotation.id}
                            </h3>
                            <div className={`px-3 py-1 rounded-full text-sm font-medium border ${getStatusColor(quotation.status)}`}>
                              <div className="flex items-center space-x-1">
                                <Icon name={getStatusIcon(quotation.status)} size={14} />
                                <span>{quotation.status.charAt(0).toUpperCase() + quotation.status.slice(1)}</span>
                              </div>
                            </div>
                            <span className={`text-sm font-medium ${getUrgencyColor(quotation.urgency)}`}>
                              {quotation.urgency.toUpperCase()} PRIORITY
                            </span>
                          </div>
                          <p className="text-base font-medium text-foreground mb-1">
                            {quotation.productName}
                          </p>
                          <p className="text-sm text-muted-foreground">
                            Quantity: {quotation.quantity} • Submitted: {new Date(quotation.date).toLocaleDateString()}
                          </p>
                        </div>
                        <div className="text-right">
                          <p className="text-sm text-muted-foreground">
                            Expected response by
                          </p>
                          <p className="text-sm font-medium text-foreground">
                            {new Date(quotation.estimatedResponse).toLocaleDateString()}
                          </p>
                        </div>
                      </div>

                      {/* Specifications */}
                      {quotation.specifications && (
                        <div className="mb-4">
                          <p className="text-sm font-medium text-foreground mb-1">Specifications:</p>
                          <p className="text-sm text-muted-foreground">{quotation.specifications}</p>
                        </div>
                      )}

                      {/* Responses */}
                      {quotation.responses?.length > 0 && (
                        <div className="mb-4">
                          <p className="text-sm font-medium text-foreground mb-3">Vendor Responses:</p>
                          <div className="space-y-3">
                            {quotation.responses.map((response, index) => (
                              <div key={index} className="bg-muted/50 rounded-lg p-4">
                                <div className="flex justify-between items-start mb-2">
                                  <p className="font-medium text-foreground">{response.vendor}</p>
                                  <p className="text-lg font-bold text-primary">${response.price.toFixed(2)}</p>
                                </div>
                                <p className="text-sm text-muted-foreground mb-1">
                                  Delivery: {response.delivery}
                                </p>
                                {response.notes && (
                                  <p className="text-sm text-muted-foreground">{response.notes}</p>
                                )}
                              </div>
                            ))}
                          </div>
                        </div>
                      )}

                      {/* Approval Status */}
                      {quotation.status === 'approved' && quotation.approvedOn && (
                        <div className="mb-4 p-3 bg-success/10 border border-success/20 rounded-lg">
                          <div className="flex items-center space-x-2">
                            <Icon name="CheckCircle" size={16} className="text-success" />
                            <p className="text-sm text-success font-medium">
                              Approved on {new Date(quotation.approvedOn).toLocaleDateString()}
                            </p>
                          </div>
                        </div>
                      )}

                      {/* Actions */}
                      <div className="flex flex-wrap gap-2 pt-4 border-t border-border">
                        <Button
                          variant="outline"
                          size="sm"
                          iconName="Eye"
                          iconPosition="left"
                        >
                          View Details
                        </Button>
                        {quotation.status === 'responded' && (
                          <Button
                            size="sm"
                            iconName="CheckCircle"
                            iconPosition="left"
                          >
                            Approve Quote
                          </Button>
                        )}
                        {quotation.status === 'pending' && (
                          <Button
                            variant="outline"
                            size="sm"
                            iconName="Edit"
                            iconPosition="left"
                          >
                            Modify Request
                          </Button>
                        )}
                        <Button
                          variant="outline"
                          size="sm"
                          iconName="MessageSquare"
                          iconPosition="left"
                        >
                          Contact Support
                        </Button>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          );
        };

        export default QuotationRequests;