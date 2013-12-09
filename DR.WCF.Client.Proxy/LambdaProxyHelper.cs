

namespace DR.WCF.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading;

    namespace WCFClient
    {
        public delegate void UseServiceDelegate<T>(T proxy);
        public delegate void UseServiceDelegateWithAsyncReturn<T>(T proxy, object obj);

        public class LamdaProxyHelper<T>
        {
            IClientChannel _proxy;

            AsyncCallback _callBack;

            UseServiceDelegate<T> _action;

            private static IDictionary<string, ChannelFactory<T>> _channelPool
                = new Dictionary<string, ChannelFactory<T>>();


            UseServiceDelegateWithAsyncReturn<T> _actionWithAsyncReturn;

            /// <summary>
            /// Returns an instance of the channel object. The channel is not yet open.
            /// </summary>
            private ChannelFactory<T> GetChannelFactory(string endPoint)
            {
                ChannelFactory<T> channelFactory = null;
                if (!_channelPool.TryGetValue(endPoint, out channelFactory))
                {
                    channelFactory = new ChannelFactory<T>(endPoint);
                    _channelPool.Add(endPoint, channelFactory);
                }
                return channelFactory;
            }

            public void Use(UseServiceDelegate<T> action, string endPoint)
            {
                try
                {
                    _proxy = GetChannelFactory(endPoint).CreateChannel() as IClientChannel;
                    if (_proxy != null)
                    {
                        _proxy.Open();
                        action((T)_proxy);

                        _proxy.Close();
                    }
                }
                catch (CommunicationException communicationException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }

                    throw communicationException;
                }
                catch (TimeoutException timeoutException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw timeoutException;
                }
                catch (Exception ex)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw ex;
                }
            }

            private void AsyncResult(IAsyncResult ar)
            {
                _action.EndInvoke(ar);
                _proxy.Close();
                _callBack(ar);
            }

            public void UseAsync(UseServiceDelegate<T> action, string endPoint, AsyncCallback callBack, object obj)
            {
                try
                {
                    _proxy = GetChannelFactory(endPoint).CreateChannel() as IClientChannel;
                    if (_proxy != null)
                    {

                        _proxy.Open();
                        _callBack = callBack;
                        _action = action;
                        IAsyncResult result = _action.BeginInvoke((T)_proxy, AsyncResult, obj);
                    }
                }
                catch (CommunicationException communicationException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw communicationException;
                }
                catch (TimeoutException timeoutException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw timeoutException;
                }
                catch (Exception ex)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw ex;
                }
            }

            public void UseAsyncWithReturnValue(UseServiceDelegateWithAsyncReturn<T> action, string endPoint, object obj)
            {
                try
                {
                    _proxy = GetChannelFactory(endPoint).CreateChannel() as IClientChannel;
                    if (_proxy != null)
                    {
                        _actionWithAsyncReturn = action;
                        new Thread(() =>
                        {
                            action((T)_proxy, obj);
                            _proxy.Close();
                        }).Start();

                    }
                }
                catch (CommunicationException communicationException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw communicationException;
                }
                catch (TimeoutException timeoutException)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw timeoutException;
                }
                catch (Exception ex)
                {
                    if (_proxy != null)
                    {
                        _proxy.Abort();
                    }
                    throw ex;
                }

            }
        }
    }

}
