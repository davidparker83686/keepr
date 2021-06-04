import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
// import { vaultsService } from './VaultsService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.push(res.data)
    // this.getKeepsByUserId()
  }

  async getKeepsByUserId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getKeepsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.accountKeeps = res.data
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(i => i.id !== id)
    AppState.activeKeep = {}
  }

  async activeKeep(keep) {
    logger.log(keep)
    AppState.activeKeep = keep

    await api.put('api/keeps/' + keep.id, keep)
    const viewedKeep = AppState.keeps.findIndex(k => k.id === keep.id)
    keep.views = keep.views + 1
    AppState.keeps[viewedKeep] = keep
  }

  async createVaultKeep(vId, kId) {
    // const newVaultKeep = await api.get(`api/keeps/${kId}`)

    const newVaultKeep = { vaultId: vId, keepId: kId }

    const res = await api.post('api/vaultkeeps', newVaultKeep)

    AppState.vaultKeeps.push(res.data)
    logger.log(AppState.vaultKeeps)
  }

  async addKeepKeepCount(id) {
    const keep = await api.get(`api/keeps/${id}`)
    keep.keeps = keep.keeps + 1
    // AppState.keeps[keep] = keep
    const keptKeep = AppState.keeps.findIndex(k => k.id === id)
    AppState.keeps[keptKeep] = keep
  }
}

export const keepsService = new KeepsService()

// vaultsService.getKeepsByVaultId(newVaultKeep.vaultId)
// newVaultKeep.vaultId = vId
