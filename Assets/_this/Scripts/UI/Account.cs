using System;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    [SerializeField] private Button profile;
    [SerializeField] private Button ticket;
    [SerializeField] private Button transactions;
    [SerializeField] private Button leaderboard;
    [SerializeField] private Button rate;
    [SerializeField] private Button support;
    [SerializeField] private Button terms;
    [SerializeField] private Button policy;
    [SerializeField] private Button settings;
    [SerializeField] private Button signOut;
    [SerializeField] private Button buyTicketBtt;
    
    public static event Action OnSignOutAction;

    private void OnEnable()
    {
        profile.onClick.AddListener(OnProfile);
        ticket.onClick.AddListener(OnTicket);
        transactions.onClick.AddListener(OnTransactions);
        leaderboard.onClick.AddListener(OnLeaderboard);
        rate.onClick.AddListener(OnRate);
        support.onClick.AddListener(OnSupport);
        terms.onClick.AddListener(OnTerms);
        policy.onClick.AddListener(OnPolicy);
        settings.onClick.AddListener(OnSettings);
        signOut.onClick.AddListener(OnSignOut);
        buyTicketBtt.onClick.AddListener(OnBuyTicketBtt);
    }

    private void OnDisable()
    {
        profile.onClick.RemoveListener(OnProfile);
        ticket.onClick.RemoveListener(OnTicket);
        transactions.onClick.RemoveListener(OnTransactions);
        leaderboard.onClick.RemoveListener(OnLeaderboard);
        rate.onClick.RemoveListener(OnRate);
        support.onClick.RemoveListener(OnSupport);
        terms.onClick.RemoveListener(OnTerms);
        policy.onClick.RemoveListener(OnPolicy);
        settings.onClick.RemoveListener(OnSettings);
        signOut.onClick.RemoveListener(OnSignOut);
        buyTicketBtt.onClick.RemoveListener(OnBuyTicketBtt);
    }

    private void OnProfile()
    {
        UIManager.LoadScreenAnimated(UIScreen.UserProfile);
    }

    private void OnTicket()
    {
        UIManager.LoadScreenAnimated(UIScreen.MyTickets);
    }

    private void OnTransactions()
    {
        UIManager.LoadScreenAnimated(UIScreen.Transaction);
    }

    private void OnLeaderboard()
    {
        UIManager.LoadScreenAnimated(UIScreen.LeaderBoards);
    }

    private void OnRate()
    {
        UIManager.LoadScreenAnimated(UIScreen.RateUs);
    }

    private void OnSupport()
    {
        UIManager.LoadScreenAnimated(UIScreen.Support);
    }

    private void OnTerms()
    {
        UIManager.LoadScreenAnimated(UIScreen.TermsAndConditions);
    }
    private void OnPolicy()
    {
        UIManager.LoadScreenAnimated(UIScreen.PrivacyPolicy);
    }

    private void OnSettings()
    {
        UIManager.LoadScreenAnimated(UIScreen.AppSettings);
    }

    private void OnSignOut()
    {
        OnSignOutAction?.Invoke();
        UIManager.LoadScreenAnimated(UIScreen.SignIn);
    }

    private void OnBuyTicketBtt()
    {
        UIManager.LoadScreenAnimated(UIScreen.JoinGiveaway);
    }
}