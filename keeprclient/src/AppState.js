import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profiles: {},
  keeps: [],
  vaults: [],
  activeProfile: {},
  accountVaults: [],
  accountKeeps: [],
  activeKeep: {},
  activeVault: {},
  vaultKeep: {},
  vaultKeeps: []
})
