import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Notification from '../utils/Notification'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async getKeepsByUserId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.push(res.data)
    // const id = newKeep.creatorId
    Notification.toast('Successfully Created Keep', 'success')
    // this.getKeepsByProfile(id)
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

  async deleteVaultKeep(id) {
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(i => i.id !== id)
  }

  async activeKeep(keep) {
    logger.log(keep)
    AppState.activeKeep = keep
    logger.log(AppState.activeKeep)
    const res = await api.get(`api/keeps/${keep.id}`)
    return res.data
  }
  // await api.put('api/keeps/' + keep.id, keep)
  // const viewedKeep = AppState.keeps.findIndex(k => k.id === keep.id)
  // keep.views = keep.views + 1
  // AppState.keeps[viewedKeep] = keep

  async addKeepKeepCount(id) {
    const keep = await api.get(`api/keeps/${id}`)
    keep.data.keeps = keep.data.keeps + 1
    await api.put('api/keeps/' + id, keep.data)
    this.getAllKeeps()

    // const keepAt = AppState.keeps.findIndex(k => k.id === activeKeep.id)
    // AppState.keeps[keepAt] = activeKeep
  }

  async createVaultKeep(vId, kId) {
    // const newVaultKeep = await api.get(`api/keeps/${kId}`)

    const newVaultKeep = { vaultId: vId, keepId: kId }

    const res = await api.post('api/vaultkeeps', newVaultKeep)

    AppState.vaultKeeps.push(res.data)
    Notification.toast('Successfully Added to Vault', 'success')
    logger.log(AppState.vaultKeeps)
  }
}

export const keepsService = new KeepsService()

// vaultsService.getKeepsByVaultId(newVaultKeep.vaultId)
// newVaultKeep.vaultId = vId
