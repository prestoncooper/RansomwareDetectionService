**Project Description**
This program detects all present and future ransomware in Windows file shares or local drives.

I made this service to monitor Windows file servers or NAS file shares. For early detection of ransomware you expect the ransomware to encrypt your sample files and you catch it in the act. This entraps ransomware because the sample files that ordinarily would be left alone become encrypted by the ransomware. **I made this program to aide system administrators monitor file servers.** I didn't make this for  average users to monitor individual computers. 

Username and password requested upon install are a domain account or local computer account for the windows service to install and run under.   The account specified will need read/write access to the file shares you want to monitor.   The user account can be changed later using the services.msc console.  Find the service called "RansomwareDetectionService".

**This program solves the following issues:**
* How do I monitor my windows file shares for ransomware with minimal performance impact?  (Compare tab and a few example files in the SourcePath)
* How do I detect a ransomware that does not create a ransom note in the file share or modify the file names in the share? (Compare tab)
* How do I automatically stop an infection from encrypting more files and only stop the user that was infected? (Compare tab - CommandProgram and the StopRansomwareInfectedUserPublic.ps1 script)
* What files and how many files are corrupted in my windows file shares?  (Audit Files tab)
* How do I check the integrity of the files in file shares? (Audit Files tab)
* What files have been recently changed or created since that last good backup? (Audit Files tab, or Compare tab for full binary comparison)
* How do I detect encrypted or corrupted zip files, word documents, excel files, or powerpoint files?  (Audit Files tab - ValidateZipFiles option)
* What files and how many were repeatedly created by the virus?  (Find Ransom Files tab)
* How do I delete the ransom note files created by the virus?  (Find Ransom Files tab)
* How do I replace the corrupted files and keep the newest good files?  (Audit Files tab)
* How do I quickly stop the Windows file server from sharing files during a virus outbreak?  ("Stop File  Sharing" button and sample script StopAllWindowsFileServersAfterRansomwareActivityDetected.cmd)
* How do I restore files when long file paths are involved?  (Audit Files tab for corrupted files, or FastCopy for full restore of all files)
* How do I find out what files have file permissions corrupted or files that are inaccessible?   (Audit Files tab - ExportUnknownToCSV)
* What files were created or modified when compared to a previous backup?  (Audit Files tab or Compare tab for full comparison)

![](Home_RansomwareDetectionServiceMain.png)

This program detects when/where ransomware has hit Windows file shares or local drives.  This program doesn't prevent ransomware infection see [http://www.questiondriven.com/2016/03/07/how-to-prevent-ransomware-infections/](http://www.questiondriven.com/2016/03/07/how-to-prevent-ransomware-infections/) for prevention recommendations.

When staff members get ransomware, you need to respond quickly to get their computer shutdown as soon as possible.  If you respond quickly enough, you can shut down the offending computer before other file shares become encrypted.  Anti-virus programs currently do not detect encrypted files written by ransomware.  Not knowing that a ransomware virus is on your network is a big problem.  The sooner you get the offending computer shutdown and restore your backups of files shares the better.  

File servers do not get the virus, the virus encrypts the files stored on the file server. This makes knowing the damage caused by a ransomware difficult. If you do not notice an encrypted file share, you can lose your opportunity to restore from backup or cause your users to use a much older backup than necessary.  Anti-virus programs are always a few days behind in detecting new viruses.   

Full Documentation [https://ransomwaredetectionservice.codeplex.com/documentation](https://ransomwaredetectionservice.codeplex.com/documentation)


**There are additional uses for this software that are not related to ransomware:**

* Search for corrupted or encrypted office documents in file shares. (Audit Files tab)
* File change email notification (Compare tab)
* File change can execute a script (Compare tab)
* Get a list of changed files when compared with last backup (Audit Files tab)
* Get a list of all unknown file extensions in file shares (Audit Files tab)
* Get a list of all files in the file share (Audit Files tab)
* Verify the binary/content of files when compared with a backup of the same files (Compare tab)

These tasks can help with damage control after an infection, or help keep your file shares maintained.


### Feedback

If you have any strange errors please make an issue, or if your tests succeed let me know by posting a comment to the beta testing thread on the discussion tab [https://ransomwaredetectionservice.codeplex.com/discussions](https://ransomwaredetectionservice.codeplex.com/discussions) on this codeplex site.

##### Author's article regarding this project [http://www.questiondriven.com/2016/02/18/beta-testing-for-ransomware-detection-in-file-share/](http://www.questiondriven.com/2016/02/18/beta-testing-for-ransomware-detection-in-file-share/)

##### Author's Information Technology blog: [http://www.questiondriven.com](http://www.questiondriven.com)



### Resources
[http://www.questiondriven.com/2016/03/07/how-to-prevent-ransomware-infections/](http://www.questiondriven.com/2016/03/07/how-to-prevent-ransomware-infections/)

