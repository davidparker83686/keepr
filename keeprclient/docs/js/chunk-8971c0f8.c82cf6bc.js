(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-8971c0f8"],{2742:function(e,t,c){},"65c8":function(e,t,c){"use strict";c("2742")},d448:function(e,t,c){"use strict";c.r(t);var a=c("7a23");const s=Object(a["G"])("data-v-85ccc59e");Object(a["t"])("data-v-85ccc59e");const l={class:"profilesPage container-fluid m-md-5"},n={class:"row"},o={class:"col-md-3 "},b={class:"col-md-8 p-0"},u={key:0,class:"biggest-font-size"},i=Object(a["g"])("br",null,null,-1),r={class:"smallest-font-size"},d=Object(a["g"])("br",null,null,-1),j={class:"smallest-font-size"},O={class:"row"},p=Object(a["g"])("div",{class:"col-12 mt-md-5"},[Object(a["g"])("span",{class:"middle-font-size"},"Vaults"),Object(a["g"])("button",{type:"button",class:"btn btn-none shadow-none text-success","data-toggle":"modal",title:"Create Vault",aria:"Create Vault","data-target":"#newVaultModal"},[Object(a["g"])("i",{class:"fas fa-plus"})])],-1),g={class:"col-12 d-flex flex-wrap justify-content-between"},f={class:"row"},v=Object(a["g"])("div",{class:"col-12"},[Object(a["g"])("span",{class:"middle-font-size"},"Keeps"),Object(a["g"])("button",{type:"button",class:"btn btn-none shadow-none text-success",title:"Create Keep",aria:"Create Vault","data-toggle":"modal","data-target":"#newKeepModal"},[Object(a["g"])("i",{class:"fas fa-plus"})])],-1),m={class:"col-12"},w={class:"card-columns"};Object(a["r"])();const K=s((e,t,c,s,K,V)=>{const k=Object(a["x"])("Vault"),P=Object(a["x"])("Keep"),y=Object(a["x"])("new-keep-modal"),z=Object(a["x"])("new-vault-modal");return Object(a["q"])(),Object(a["d"])("div",l,[Object(a["g"])("div",n,[Object(a["g"])("div",o,[Object(a["g"])("img",{src:s.state.activeProfile.picture,class:" imgsize",alt:"Profile Pic"},null,8,["src"])]),Object(a["g"])("div",b,[s.state.account?(Object(a["q"])(),Object(a["d"])("span",u,Object(a["z"])(s.state.activeProfile.name),1)):Object(a["e"])("",!0),i,Object(a["g"])("span",r,"Vaults:"+Object(a["z"])(s.getUserVaults()),1),d,Object(a["g"])("span",j,"Keeps:"+Object(a["z"])(s.getUserKeeps()),1)])]),Object(a["g"])("div",O,[p,Object(a["g"])("div",g,[(Object(a["q"])(!0),Object(a["d"])(a["a"],null,Object(a["w"])(s.state.accountVaults,e=>(Object(a["q"])(),Object(a["d"])(k,{key:e.id,vault:e},null,8,["vault"]))),128))])]),Object(a["g"])("div",f,[v,Object(a["g"])("div",m,[Object(a["g"])("div",w,[(Object(a["q"])(!0),Object(a["d"])(a["a"],null,Object(a["w"])(s.state.accountKeeps,e=>(Object(a["q"])(),Object(a["d"])(P,{key:e.id,keep:e},null,8,["keep"]))),128))])])]),Object(a["g"])(y),Object(a["g"])(z)])});var V=c("83fc"),k=c("6c02"),P=c("473f"),y=c("6c96"),z=c("d10d"),x=c("416b"),h={components:{NewKeepModal:x["default"]},name:"Profiles",setup(){const e=Object(k["c"])(),t=Object(a["u"])({keeps:Object(a["b"])(()=>V["a"].keeps),vaults:Object(a["b"])(()=>V["a"].vaults),activeProfile:Object(a["b"])(()=>V["a"].activeProfile),accountVaults:Object(a["b"])(()=>V["a"].accountVaults),accountKeeps:Object(a["b"])(()=>V["a"].accountKeeps),account:Object(a["b"])(()=>V["a"].account),user:Object(a["b"])(()=>V["a"].user)});return Object(a["o"])(async()=>{try{await y["a"].getKeepsByProfile(e.params.id),await P["a"].makeActiveProfile(e.params.id),await y["a"].getKeepsByProfile(e.params.id),await z["a"].getVaultsByProfile(e.params.id,t.account.id)}catch(c){console.error(c)}}),{state:t,route:e,getUserKeeps(){const e=t.accountKeeps.length;return e},getUserVaults(){const e=t.accountVaults.length;return e}}}};c("65c8");h.render=K,h.__scopeId="data-v-85ccc59e";t["default"]=h}}]);