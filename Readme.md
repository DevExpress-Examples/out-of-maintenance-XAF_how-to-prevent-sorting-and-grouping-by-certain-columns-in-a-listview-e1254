<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592351/11.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1254)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [WebAllowSortListViewController.cs](./CS/WinWebSolution.Module.Web/WebAllowSortListViewController.cs) (VB: [WebAllowSortListViewController.vb](./VB/WinWebSolution.Module.Web/WebAllowSortListViewController.vb))
* [WinAllowSortListViewController.cs](./CS/WinWebSolution.Module.Win/WinAllowSortListViewController.cs) (VB: [WinAllowSortListViewController.vb](./VB/WinWebSolution.Module.Win/WinAllowSortListViewController.vb))
* **[AllowSortListViewController.cs](./CS/WinWebSolution.Module/AllowSortListViewController.cs) (VB: [AllowSortListViewController.vb](./VB/WinWebSolution.Module/AllowSortListViewController.vb))**
* [Demo.cs](./CS/WinWebSolution.Module/Demo.cs) (VB: [Demo.vb](./VB/WinWebSolution.Module/Demo.vb))
* [ModelExtenders.cs](./CS/WinWebSolution.Module/ModelExtenders.cs) (VB: [ModelExtenders.vb](./VB/WinWebSolution.Module/ModelExtenders.vb))
* [Module.cs](./CS/WinWebSolution.Module/Module.cs) (VB: [Module.vb](./VB/WinWebSolution.Module/Module.vb))
<!-- default file list end -->
# How to prevent sorting and grouping by certain columns in a ListView
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e1254)**
<!-- run online end -->


<p>The following features are implemented in this example:</p><p>1. <strong>ListViewColumnOptionsAttribute</strong> - this is a regular attribute class that can be used in your code to mark a business class property, to specify whether you want to allow the corresponding ListView column to be sorted or grouped.</p><p>For a demonstration on how to use this attribute see the TestObject class.</p><p>2. <strong>AllowSort</strong>  and <strong>AllowGroup</strong> - these are new attributes declared on the <i>BOModel | Member</i> and <i>Views | ListView and ListView | Columns | Column</i> nodes levels in the application model so that you can specify them via the Model Editor. These attributes are provided by the <strong>IModelListViewColumnOptions</strong> model extension. Refer to the <a href="http://documentation.devexpress.com/#Xaf/CustomDocument2785"><u>How to: Extend the Application Model</u></a> help article to learn more on how this feature is implemented.</p><p>3. <strong>ListViewColumnOptionsController</strong> - this is a platform-agnostic ViewController that reads values of the AllowSort and AllowGroup attributes from the application model and applies them to the ListView columns through the ColumnsListEditor class. That means that all ColumnsListEditor descendants such as GridListEditor, ASPxGridListEditor, TreeListEditor and ASPxTreeListEditor will be affected by this functionality.</p><p>Refer to the <a href="http://www.screencast.com/t/8ySPSwfkhol"><u>http://www.screencast.com/t/8ySPSwfkhol</u></a> video to see how this works in action.</p><p><strong>See Also:</strong><br />
<a href="https://www.devexpress.com/Support/Center/p/E1253">How to sort a nested ListView at the business objects level, in code</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E1276">How to sort a ListView in code</a><br />
<a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument3499"><u>XtraGrid | Sorting</u></a><br />
<a href="http://documentation.devexpress.com/#AspNet/CustomDocument3714"><u>ASPxGridView | Sorting</u></a></p>

<br/>


